import { useEffect, useState } from "react";
import Navbar from "./Components/Navbar";

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

  const logout = () => {
    localStorage.setItem("userToken", "");
    setUser(null);
  }

  useEffect(() => {
    //fetchUser().then(usr => setUser(usr));
    setUser({ userName: "asd", id: "1a" });
    //setUser(null);
  }, []);

  return (
    <div>
      <Navbar user={user} logout={logout} />
    </div>
  );
}

export default App;
