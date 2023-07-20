import { HubConnection, HubConnectionBuilder, HubConnectionState, LogLevel } from "@microsoft/signalr";
import { useEffect, useState } from "react";

function UserMainPage() {
    const [connection, setConnection] = useState<HubConnection | null>(null);

    useEffect(() => {
        const hubConnection = new HubConnectionBuilder()
            .withUrl("/api/Chat")
            .configureLogging(LogLevel.Information)
            .withAutomaticReconnect()
            .build();
        setConnection(hubConnection);

        () => setConnection(null);
    }, []);

    useEffect(() => {
        if (!connection) {
            return;
        }
        connection?.on("ReceivePing", (msg) => {
            console.log("Ping message received from SignalR: " + msg);
        });
        if (connection.state === HubConnectionState.Disconnected) {
            connection?.start()
                .then(() => connection?.invoke("PingAll", "Hello, SignalR!"))
        }

    }, [connection]);

    return (
        <div>User Main Page</div>
    );
}

export default UserMainPage;