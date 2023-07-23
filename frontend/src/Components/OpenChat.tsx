import { Dispatch, SetStateAction, useEffect } from "react";
import ChatRoom from "../types/ChatRoom";

function OpenChat({ room, setRoom }: {room: ChatRoom | null, setRoom: Dispatch<SetStateAction<ChatRoom | null>>}) {
    useEffect(() => {
        console.log(room);
    }, [room]);

    return (
        <div className="w-5/6 border">Open Chat</div>
    );
}

export default OpenChat;
