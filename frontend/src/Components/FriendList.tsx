import React, { useEffect } from "react";

function FriendList({ friends, setFriends, openDirectChatRoom }: {
    friends: Array<User>,
    setFriends: React.Dispatch<React.SetStateAction<User[]>>,
    openDirectChatRoom: (friendId: string) => void
}){
    const fetchFriends = () => {
        const token: string = localStorage.getItem("userToken") ?? "";
        if (token === "") {
            return;
        }
        fetch("/api/User/GetFriends/" + token)
            .then(response => response.json())
            .then(data => setFriends(data));
    };

    useEffect(() => {
        fetchFriends();   
    }, []);

    return (
        <div className="w-1/6 border">
            <ul className="flex flex-col overflow-y-scroll">
                {friends.map(friend => 
                    <div key={friend.id}
                    onClick={() => openDirectChatRoom(friend.id)}
                    className="cursor-pointer my-1 hover:underline">{friend.userName}</div>)}
            </ul>
        </div>
    );
}

export default FriendList;
