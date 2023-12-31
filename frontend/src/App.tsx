import { useEffect, useState } from "react";
import Navbar from "./Components/Navbar";
import EmptyPage from "./Components/EmptyPage";
import UserMainPage from "./Components/UserMainPage";

async function fetchUser(): Promise<User | null> {
  let jwt = localStorage.getItem("userToken");
  if (!jwt) {
    return null;
  }

  return fetch(`/api/User/GetByJwt/${jwt}`)
    .then(res => res.json());
}

function App() {
  let [user, setUser] = useState<User | null>(null);
  let [page, setPage] = useState(<EmptyPage />);

  const logout = () => {
    localStorage.setItem("userToken", "");
    setUser(null);
  }

  useEffect(() => {
    fetchUser().then(usr => setUser(usr));
  }, []);
  useEffect(() => {
    if (user) {
      setPage(<UserMainPage user={user}/>);
    } else {
      setPage(<EmptyPage />)
    }
  }, [user]);

  return (
    <div className="h-screen">
      <Navbar user={user} logout={logout} setPage={setPage} />
      {page}
    </div>
  );
}

export default App;
