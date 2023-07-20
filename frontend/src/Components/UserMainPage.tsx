import { HubConnection, HubConnectionBuilder, HubConnectionState, LogLevel } from "@microsoft/signalr";
import { useEffect, useState } from "react";
import FriendRequests from "./FriendRequests";
import OpenChat from "./OpenChat";
import FriendList from "./FriendList";

function UserMainPage() {
    const [chatConnection, setChatConnection] = useState<HubConnection | null>(null);
    const [friendConnection, setFriendConnection] = useState<HubConnection | null>(null);

    useEffect(() => {
        const hubConnection = new HubConnectionBuilder()
            .withUrl("/api/Chat")
            .configureLogging(LogLevel.Information)
            .withAutomaticReconnect()
            .build();
        setChatConnection(hubConnection);

        const hubConnection2 = new HubConnectionBuilder()
            .withUrl("/api/Friend")
            .configureLogging(LogLevel.Information)
            .withAutomaticReconnect()
            .build();
        setFriendConnection(hubConnection2);

        () => {
            setChatConnection(null);
            setFriendConnection(null);
        };
    }, []);

    useEffect(() => {
        if (!chatConnection) {
            return;
        }
        chatConnection?.on("ReceivePing", (msg) => {
            console.log("Ping message received from SignalR: " + msg);
        });
        if (chatConnection.state === HubConnectionState.Disconnected) {
            chatConnection?.start()
                .then(() => chatConnection?.invoke("PingAll", "Hello, SignalR!"))
        }

    }, [chatConnection]);

    useEffect(() => {
        if (!friendConnection) {
            return;
        }
        if (friendConnection?.state === HubConnectionState.Disconnected) {
            friendConnection.start()
                .then(() => friendConnection.invoke("AddToSelfGroup", localStorage.getItem("userToken")));
        }
    });

    return (
        <div className="flex flex-col h-full">
            <div className="flex flex-row h-full">
                <OpenChat />
                <FriendList />
            </div>
            <FriendRequests />
        </div>
    );
}

export default UserMainPage;