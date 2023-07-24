import { HubConnection } from "@microsoft/signalr";
import { useEffect, useState } from "react";

function FriendRequests({ friendConnection, friendRequests, setFriendRequests }: {
    friendConnection: HubConnection | null,
    friendRequests: Array<User>,
    setFriendRequests: React.Dispatch<React.SetStateAction<User[]>>
}) {
    const [userNameInput, setUserNameInput] = useState("");


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

    const acceptFriendRequest = (requesterId: string) => {
        const token = localStorage.getItem("userToken") ?? "";
        if (token === "") {
            return;
        }
        friendConnection?.invoke("AcceptFriendRequest", token, requesterId);
    };

    useEffect(() => {
        fetchFriendRequests();
    }, []);

    return (
        <div className="h-20 m-auto">
            <input type="text" placeholder="Send a friend request to a user" className="border p-2"
                onChange={e => setUserNameInput(e.target.value)} />
            <button className="btn btn-blue" onClick={sendFriendRequest}>Request friend</button>
            <ul className="flex flex-col">
                {friendRequests.map(usr => {
                    return (
                        <li key={usr.id} className="m-auto">
                            <button onClick={() => acceptFriendRequest(usr.id)} className="btn btn-blue !font-normal m-1">
                                Accept <span className="font-bold">{usr.userName}</span>'s friend request
                            </button>
                        </li>
                    )
                })}
            </ul>
        </div>
    );
}

export default FriendRequests;
