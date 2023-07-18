function Navbar({ user, logout }: { user: User | null, logout: () => void }) {
    return (
        <nav className="flex flex-row justify-evenly">
            <div className="">{user ? <div>Logged in as <b>{user.userName}</b></div> : "Logged out"}</div>
            {user
                ? <button className="btn btn-blue" onClick={logout}>Log out</button>
                : <><button className="btn btn-blue">Log in</button>
                    <button className="btn btn-blue">Register</button></>}
        </nav>
    );
}

export default Navbar;
