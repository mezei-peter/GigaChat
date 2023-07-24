import { FormEvent, useEffect, useState } from "react";
import ChatRoom from "../types/ChatRoom";
import { HubConnection } from "@microsoft/signalr";

function OpenChat({ room, user, chatConnection }: {
    room: ChatRoom | null,
    user: User,
    chatConnection: HubConnection | null
}) {
    const [messageDraft, setMessageDraft] = useState<string>("");

    useEffect(() => {
        chatConnection?.invoke("AddToChatRoomGroup", localStorage.getItem("userToken"), room?.room.id);
    }, [room]);

    const sendMessage = (e: FormEvent) => {
        e.preventDefault();
        if (messageDraft) {
            chatConnection?.invoke("SendMessageToChatRoom", localStorage.getItem("userToken"), room?.room.id, messageDraft);
            setMessageDraft("");
        }    
    };

    return (
        <div className="flex flex-col w-5/6 border justify-between">
            <div className="text-center text-lg bg-gray-100">
                {room?.room.type === 0 ? room?.room.name?.replace(`${user.userName}-`, "").replace(`-${user.userName}`, "") : room?.room.name}
            </div>
            <div className="flex flex-col overflow-y-scroll h-full">
            {room !== null && 
                (room?.messages.map(msg => (
                    <div 
                        key={msg.id} 
                        className={"flex flex-col border w-4/6 m-1 p-1"
                            .concat(msg.author.id === user.id ? " bg-sky-100 self-end" : " bg-gray-100 self-start")}
                    >
                        <div className="break-words">{msg.message}</div>  
                        <div className="flex flex-row justify-between"> 
                            <div className="text-xs">{`${msg.dateTime.slice(0, 10)} ${msg.dateTime.slice(11, 19)}`}</div>
                            <div className="text-xs">--{msg.author.userName}</div>
                        </div>
                    </div>
                )))
            }
            </div>
            <form onSubmit={(e) => sendMessage(e)}>
                <input type="text" placeholder="Write a message" 
                    className="border p-1 w-5/6"
                    onChange={e => setMessageDraft(e.target.value)}    
                    value={messageDraft}
                />
                <button className="btn btn-blue w-1/6">Send</button> 
            </form>
        </div>
    );
}

export default OpenChat;
