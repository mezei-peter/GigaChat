function FriendList({friends}: {friends: Array<User>}) {
    return (
        <div className="w-1/6 border">
            <ul className="flex flex-col">
                {friends.map(friend => <div key={friend.id}>{friend.userName}</div>)}
            </ul>
        </div>
    );
}

export default FriendList;
