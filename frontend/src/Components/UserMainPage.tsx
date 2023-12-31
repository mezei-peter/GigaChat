import { HubConnection, HubConnectionBuilder, HubConnectionState, LogLevel } from "@microsoft/signalr";
import { useEffect, useState } from "react";
import FriendRequests from "./FriendRequests";
import OpenChat from "./OpenChat";
import FriendList from "./FriendList";
import ChatRoom from "../types/ChatRoom";
import ChatMessage from "../types/ChatMessage";

function UserMainPage({user}: {user: User}) {
    const [chatConnection, setChatConnection] = useState<HubConnection | null>(null);
    const [friendConnection, setFriendConnection] = useState<HubConnection | null>(null);
    const [friendRequests, setFriendRequests] = useState<Array<User>>([]);
    const [friends, setFriends] = useState<Array<User>>([]);
    const [openRoom, setOpenRoom] = useState<ChatRoom | null>(null);

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
        chatConnection?.on("ReceiveMessageToChatRoom", (msg: ChatMessage) => {
            setOpenRoom((curr: ChatRoom | null) => {
                return curr ? {...(curr as ChatRoom), messages: [...curr.messages, msg]} : null;
            });
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
        friendConnection?.on("ReceiveFriendRequest", (id, userName) => {
            setFriendRequests((frs) => [{ id: id, userName: userName }, ...frs]);
        });
        friendConnection?.on("AddFriend", (id, userName) => {
            setFriends((fs) => [{id: id, userName: userName}, ...fs]); 
            setFriendRequests((frs) => [...frs.filter(fr => fr.id !== id)]);
        });
        if (friendConnection?.state === HubConnectionState.Disconnected) {
            friendConnection.start()
                .then(() => friendConnection.invoke("AddToSelfGroup", localStorage.getItem("userToken")));
        }
    }, [friendConnection]);

    const openDirectChatRoom = (friendId: string) => {
        fetch(`/api/Chat/GetDirectChatRoom/${localStorage.getItem("userToken")}/${friendId}`)
            .then(res => res.json())
            .then(data => setOpenRoom(data));
    }
    
    return (
        <div className="flex flex-col h-full">
            <div className="flex flex-row h-5/6">
                <OpenChat room={openRoom} user={user} chatConnection={chatConnection}/>
                <FriendList friends={friends} setFriends={setFriends} openDirectChatRoom={openDirectChatRoom} />
            </div>
            <FriendRequests friendConnection={friendConnection} friendRequests={friendRequests} setFriendRequests={setFriendRequests} />
        </div>
    );
}

export default UserMainPage;
