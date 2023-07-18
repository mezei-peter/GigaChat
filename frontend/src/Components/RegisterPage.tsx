function RegisterPage() {
    return (
        <form className="flex flex-col justify-center h-2/4 w-2/4 m-auto">
            <h3 className="text-4xl">Register</h3>
            <input type="text" placeholder="Username" className="border p-1 m-1" />
            <input type="password" placeholder="Password" className="border p-1 m-1" />
            <button type="button" className="btn btn-blue">Submit</button>
        </form>
    );
}

export default RegisterPage;