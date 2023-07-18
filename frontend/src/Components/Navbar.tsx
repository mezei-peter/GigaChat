import LoginPage from "./LoginPage";
import RegisterPage from "./RegisterPage";

function Navbar({ user, logout, setPage }: {
    user: User | null,
    logout: () => void,
    setPage: React.Dispatch<React.SetStateAction<JSX.Element>>
}) {
    return (
        <nav className="flex flex-row justify-evenly">
            <div className="">{user ? <div>Logged in as <b>{user.userName}</b></div> : "Logged out"}</div>
            {user
                ? <button className="btn btn-blue" onClick={logout}>Log out</button>
                : <><button className="btn btn-blue" onClick={() => setPage(<LoginPage />)}>Log in</button>
                    <button className="btn btn-blue" onClick={() => setPage(<RegisterPage />)}>Register</button></>}
        </nav>
    );
}

export default Navbar;
