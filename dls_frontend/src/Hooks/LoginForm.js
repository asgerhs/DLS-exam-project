import {useState} from 'react'

function LoginForm({Login, error}) {
    const [details, setDetails] = useState({username: "", password: ""});
    const [user, setUser] = useState();

    const submitHandler = e => {
        e.preventDefault();
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json'},
            body: JSON.stringify({ username: details.username, password: details.password })
        };

        async function wait () {
            await fetch("http://localhost:8000/user/login", requestOptions)
                .then(res => setUser({username: res.username, isTeacher: res.isTeacher}))
        
        }
        wait();
        Login(user);
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
