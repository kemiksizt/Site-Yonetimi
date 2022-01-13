import React, {SyntheticEvent, useState} from 'react';
import { Navigate } from "react-router-dom";

const Login = () => {

    const [name, setName] = useState('');
    const [password, setPassword] = useState('');
    const [navigate, setNavigate] = useState(false);

    const submit = async (e: SyntheticEvent) => {
        e.preventDefault();

        await fetch('https://localhost:44377/login', {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            credentials : 'include',
            body: JSON.stringify({
                name,
                password
            })
        }); 

        setNavigate(true);
    }

    if(navigate){
        return <Navigate to = "/" />;
    }

    return (
        <form onSubmit={submit}>
            <h1 className="h3 mb-3 fw-normal">Please sign in</h1>
              <input className="form-control" placeholder="Name" required
                onChange={e => setName(e.target.value)}
              />
              
              <input type="password" className="form-control" placeholder="Password" required
                onChange={e => setPassword(e.target.value)}
              /> 
              
              <button className="w-100 btn btn-lg btn-primary" type="submit">Sign in</button>
          </form>
    );
};

export default Login;