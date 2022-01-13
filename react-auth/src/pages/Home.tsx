import React, {useEffect} from "react";

const Home = () => {

    useEffect( () =>{
        (
            async() => {
                await fetch('https://localhost:44377/user', {
                    headers: {'Content-Type': 'application/json'},
                    credentials : 'include'
                }); 
            }
        )()
    });

    return(
        <div>
            Home
        </div>
    );
};

export default Home;