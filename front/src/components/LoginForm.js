import React from 'react';

export default function LoginForm({ onLogin }) {
    return (
        <div id="login">
            <h1>Login</h1>
            <form onSubmit={(e)=>{
                e.preventDefault();
                onLogin(e.target.login.value, e.target.password.value);
            }}>
                <input name="login" placeholder="Utilisateur"/>
                <input name="password" placeholder="Mot de passe" type="password"/>
                <button type="submit">Se connecter</button>
            </form>
        </div>
    );
}
