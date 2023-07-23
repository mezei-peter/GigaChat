import { Dispatch, SetStateAction, useEffect } from "react";
import ChatRoom from "../types/ChatRoom";

function OpenChat({ room, setRoom }: {room: ChatRoom | null, setRoom: Dispatch<SetStateAction<ChatRoom | null>>}) {
    useEffect(() => {
        console.log(room);
    }, [room]);

    return (
        <div className="flex flex-col w-5/6 border justify-between">
            <div className="text-center text-lg">Chat</div>
            <div className="flex flex-col overflow-y-scroll">
                    
          
                    <div className="flex flex-col border bg-gray-100 w-4/6 self-start m-1 p-1">
                        <div className="break-words">message testsdasadasdassdfsahsavjasvnasfnvjadhsdnbjasnfkvnsakbndifvainfvjdnfvkjdnbkjnsdkjvnadkjfnbdjkbnkdjfvnds</div>  
                        <div className="flex flex-row justify-between"> 
                            <div className="text-xs">2023-05-05</div>
                            <div className="text-xs">--nobody</div>
                        </div>
                    </div>


                    <div className="flex flex-col border bg-sky-100 w-4/6 self-end m-1 p-1">
                        <div className="break-words">message test</div>  
                        <div className="flex flex-row justify-between"> 
                            <div className="text-xs">2023-05-05</div>
                            <div className="text-xs">--nobody</div>
                        </div>
                    </div>


                    <div className="flex flex-col border bg-sky-100 w-4/6 self-end m-1 p-1">
                        <div className="break-words">message test</div>  
                        <div className="flex flex-row justify-between"> 
                            <div className="text-xs">2023-05-05</div>
                            <div className="text-xs">--nobody</div>
                        </div>
                    </div>


                    <div className="flex flex-col border bg-sky-100 w-4/6 self-end m-1 p-1">
                        <div className="break-words">message test</div>  
                        <div className="flex flex-row justify-between"> 
                            <div className="text-xs">2023-05-05</div>
                            <div className="text-xs">--nobody</div>
                        </div>
                    </div>

                    <div className="flex flex-col border bg-gray-100 w-4/6 self-start m-1 p-1">
                        <div className="break-words">message testsdasadasdassdfsahsavjasvnasfnvjadhsdnbjasnfkvnsakbndifvainfvjdnfvkjdnbkjnsdkjvnadkjfnbdjkbnkdjfvnds</div>  
                        <div className="flex flex-row justify-between"> 
                            <div className="text-xs">2023-05-05</div>
                            <div className="text-xs">--nobody</div>
                        </div>
                    </div>


                    <div className="flex flex-col border bg-sky-100 w-4/6 self-end m-1 p-1">
                        <div className="break-words">message test</div>  
                        <div className="flex flex-row justify-between"> 
                            <div className="text-xs">2023-05-05</div>
                            <div className="text-xs">--nobody</div>
                        </div>
                    </div>


                    <div className="flex flex-col border bg-sky-100 w-4/6 self-end m-1 p-1">
                        <div className="break-words">message test</div>  
                        <div className="flex flex-row justify-between"> 
                            <div className="text-xs">2023-05-05</div>
                            <div className="text-xs">--nobody</div>
                        </div>
                    </div>


                    <div className="flex flex-col border bg-sky-100 w-4/6 self-end m-1 p-1">
                        <div className="break-words">message test</div>  
                        <div className="flex flex-row justify-between"> 
                            <div className="text-xs">2023-05-05</div>
                            <div className="text-xs">--nobody</div>
                        </div>
                    </div>

                    <div className="flex flex-col border bg-gray-100 w-4/6 self-start m-1 p-1">
                        <div className="break-words">message testsdasadasdassdfsahsavjasvnasfnvjadhsdnbjasnfkvnsakbndifvainfvjdnfvkjdnbkjnsdkjvnadkjfnbdjkbnkdjfvnds</div>  
                        <div className="flex flex-row justify-between"> 
                            <div className="text-xs">2023-05-05</div>
                            <div className="text-xs">--nobody</div>
                        </div>
                    </div>


                    <div className="flex flex-col border bg-sky-100 w-4/6 self-end m-1 p-1">
                        <div className="break-words">message test</div>  
                        <div className="flex flex-row justify-between"> 
                            <div className="text-xs">2023-05-05</div>
                            <div className="text-xs">--nobody</div>
                        </div>
                    </div>


                    <div className="flex flex-col border bg-sky-100 w-4/6 self-end m-1 p-1">
                        <div className="break-words">message test</div>  
                        <div className="flex flex-row justify-between"> 
                            <div className="text-xs">2023-05-05</div>
                            <div className="text-xs">--nobody</div>
                        </div>
                    </div>


                    <div className="flex flex-col border bg-sky-100 w-4/6 self-end m-1 p-1">
                        <div className="break-words">message test</div>  
                        <div className="flex flex-row justify-between"> 
                            <div className="text-xs">2023-05-05</div>
                            <div className="text-xs">--nobody</div>
                        </div>
                    </div>

                    <div className="flex flex-col border bg-gray-100 w-4/6 self-start m-1 p-1">
                        <div className="break-words">message testsdasadasdassdfsahsavjasvnasfnvjadhsdnbjasnfkvnsakbndifvainfvjdnfvkjdnbkjnsdkjvnadkjfnbdjkbnkdjfvnds</div>  
                        <div className="flex flex-row justify-between"> 
                            <div className="text-xs">2023-05-05</div>
                            <div className="text-xs">--nobody</div>
                        </div>
                    </div>


                    <div className="flex flex-col border bg-sky-100 w-4/6 self-end m-1 p-1">
                        <div className="break-words">message test</div>  
                        <div className="flex flex-row justify-between"> 
                            <div className="text-xs">2023-05-05</div>
                            <div className="text-xs">--nobody</div>
                        </div>
                    </div>


                    <div className="flex flex-col border bg-sky-100 w-4/6 self-end m-1 p-1">
                        <div className="break-words">message test</div>  
                        <div className="flex flex-row justify-between"> 
                            <div className="text-xs">2023-05-05</div>
                            <div className="text-xs">--nobody</div>
                        </div>
                    </div>


                    <div className="flex flex-col border bg-sky-100 w-4/6 self-end m-1 p-1">
                        <div className="break-words">message test</div>  
                        <div className="flex flex-row justify-between"> 
                            <div className="text-xs">2023-05-05</div>
                            <div className="text-xs">--nobody</div>
                        </div>
                    </div>


            {room !== null && 
                (room?.messages.map(msg => (
                    <div key={msg.id} className="flex flex-col">
                        <div>{msg.message}</div>  
                        <div className="flex flex-row justify-between"> 
                            <div>{msg.dateTime}</div>
                            <div>--{msg.author.userName}</div>
                        </div>
                    </div>
                )))
            }
            </div>
            <form>
                <input type="text" placeholder="Write a message" className="border p-1 w-5/6" />
                <button className="btn btn-blue w-1/6">Send</button> 
            </form>
        </div>
    );
}

export default OpenChat;
