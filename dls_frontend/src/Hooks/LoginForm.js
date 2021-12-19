import {useState} from 'react'

function LoginForm({Login, error}) {
    const [details, setDetails] = useState({username: "", password: ""});
    const [user, setUser] = useState({username: "", isTeacher: false});
   

    const submitHandler = e => {
        e.preventDefault();
        async function fetchUser () {
            const requestOptions = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json'},
                body: JSON.stringify({ username: details.username, password: details.password })
            };
            const response = await fetch("http://localhost:8000/user/login", requestOptions)
            const json = await response.json()
            json["isTeacher"] 
            ? setUser({username: json["teacher"]["email"], isTeacher: true})
            : setUser({username: json["student"]["email"], isTeacher: false})
            Login(user);
        }
        fetchUser();
    }

    const onChange = (evt) => {
        setDetails({ ...details, [evt.target.id]: evt.target.value });
    }


    return (
        <div className="AppFirst">
            <form onSubmit={submitHandler} onChange={onChange}>
                <div className="form-inner">
                    <h2>Login</h2>
                    {(error != "") ? ( <div className="error">{error}</div>) : ""}
                    <div className="form-group">
                        <label htmlFor="username">Username:</label>
                        <input type="username" name="username" id="username" />
                    </div>
                    <div className="form-group">
                        <label htmlFor="password">Password:</label>
                        <input type="password" name="password" id="password"/>
                    </div>
                    <input type="submit" value="LOGIN" />
                </div>
            </form>
        </div>
    )
}




export default LoginForm
