import ChatMessage from "./ChatMessage"

type Room = {
    id: string,
    name: string | null,
    type: number
}

type ChatRoom = {
    room: Room,
    messages: Array<ChatMessage>
}

export default ChatRoom;

