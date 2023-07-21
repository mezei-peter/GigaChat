import React, { useEffect } from "react";

function FriendList({ friends, setFriends }: { friends: Array<User>, setFriends: React.Dispatch<React.SetStateAction<User[]>> }) {
    
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
            <ul className="flex flex-col">
                {friends.map(friend => <div key={friend.id}>{friend.userName}</div>)}
            </ul>
        </div>
    );
}

export default FriendList;
