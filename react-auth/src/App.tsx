import React, { useEffect, useState } from 'react';
import './App.css';
import Login from "./pages/Login";
import Nav from "./components/Nav";
import { BrowserRouter, Route} from "react-router-dom";
import Home from "./pages/Home";
import Register from "./pages/Register";

function App() {

  const [name, setName] = useState('');

  useEffect( () =>{
      (
          async() => {
              const response = await fetch('https://localhost:44377/user', {
                  headers: {'Content-Type': 'application/json'},
                  credentials : 'include'
              }); 

              const content = await response.json();

              setName(content.name);

          }
      )();
  });

  return (
    
      <div className="App">
      
        <Nav name={name} setName={setName}/>

          <main className="form-signin">
          <BrowserRouter>
                <Route path="/" exact component={ () => <Home name={name}/>} />
                <Route path="/login" component = { () => <Login setName={setName}/>} />
                <Route path="/register" component={Register} />
                </BrowserRouter>
          </main>

      </div>
      
  );
}

export default App;