import React, { useState } from "react";

function RegisterPage() {
    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        const data = JSON.stringify({ userName: userName, password: password });
        fetch("/api/User/PostNewUser", { method: "POST", headers: { 'Content-type': 'application/json' }, body: data })
            .then(res => res.json())
            .then(data => console.log(data));
    };

    return (
        <form className="flex flex-col justify-center h-2/4 w-2/4 m-auto" onSubmit={e => handleSubmit(e)}>
            <h3 className="text-4xl">Register</h3>
            <input type="text" placeholder="Username" className="border p-1 m-1"
                onChange={e => setUserName(e.target.value)} />
            <input type="password" placeholder="Password" className="border p-1 m-1"
                onChange={e => setPassword(e.target.value)} />
            <button type="submit" className="btn btn-blue">Submit</button>
        </form>
    );
}

export default RegisterPage;