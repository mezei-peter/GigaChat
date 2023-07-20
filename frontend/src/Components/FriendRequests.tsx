import { HubConnection } from "@microsoft/signalr";
import { useEffect, useState } from "react";

function FriendRequests({ friendConnection }: { friendConnection: HubConnection | null }) {
    const [userNameInput, setUserNameInput] = useState("");
    const [friendRequests, setFriendRequests] = useState<Array<User>>([]);


    const sendFriendRequest = () => {
        const token: string = localStorage.getItem("userToken") ?? "";
        if (token === "") {
            return;
        }
        friendConnection?.invoke("SendFriendRequest", token, userNameInput);
    };

    const fetchFriendRequests = () => {
        const token: string = localStorage.getItem("userToken") ?? "";
        if (token === "") {
            return;
        }
        fetch("/api/User/GetFriendRequests/" + token)
            .then(response => response.json())
            .then(data => setFriendRequests(data));
    };

    useEffect(() => {
        console.log("Friends requests mounted");
        fetchFriendRequests();
    }, []);

    return (
        <div className="h-20">
            <input type="text" placeholder="Send a friend request to a user" className="border p-2"
                onChange={e => setUserNameInput(e.target.value)} />
            <button className="btn btn-blue" onClick={sendFriendRequest}>Request friend</button>
            <ul className="flex flex-col">
                {friendRequests.map(usr => {
                    return (
                        <li key={usr.id} className="flex flex-row justify-between">
                            <div><span className="font-bold">{usr.userName}</span> wants to be your friend</div>
                            <button className="btn btn-blue" type="button">Accept</button>
                        </li>
                    )
                })}
            </ul>
        </div>
    );
}

export default FriendRequests;