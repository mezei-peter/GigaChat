import { HubConnection } from "@microsoft/signalr";
import { useState } from "react";

function FriendRequests({ friendConnection }: { friendConnection: HubConnection | null }) {
    const [userNameInput, setUserNameInput] = useState("");

    const sendFriendRequest = () => {
        const token: string = localStorage.getItem("userToken") ?? "";
        if (token === "") {
            return;
        }
        friendConnection?.invoke("SendFriendRequest", token, userNameInput);
    };

    return (
        <div className="border h-20">
            <input type="text" placeholder="Send a friend request to a user" className="border p-2"
                onChange={e => setUserNameInput(e.target.value)} />
            <button className="btn btn-blue" onClick={sendFriendRequest}>Request friend</button>
        </div>
    );
}

export default FriendRequests;