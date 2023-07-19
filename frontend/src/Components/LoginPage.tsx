import { useState } from "react";

function LoginPage() {
    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");

    const handleSubmit = (event: React.FormEvent) => {
        event.preventDefault();
        const data = JSON.stringify({ userName: userName, password: password });
        fetch("/api/User/Login", { method: "POST", headers: { 'Content-type': 'application/json' }, body: data })
            .then(res => res.text())
            .then(data => localStorage.setItem("userToken", data));
    };

    return (
        <form className="flex flex-col justify-center h-2/4 w-2/4 m-auto" onSubmit={e => handleSubmit(e)}>
            <h3 className="text-4xl">Login</h3>
            <input type="text" placeholder="Username" className="border p-1 m-1"
                onChange={e => setUserName(e.target.value)} />
            <input type="password" placeholder="Password" className="border p-1 m-1"
                onChange={e => setPassword(e.target.value)} />
            <button type="submit" className="btn btn-blue">Log in</button>
        </form>
    );
}

export default LoginPage;