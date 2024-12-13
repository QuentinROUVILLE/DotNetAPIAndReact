import {fetchAPI} from "../httpcommons";

export async function loginRequest(login, password) {
    return await fetchAPI(`/api/v1/auth`, {
        method: 'POST',
        body: JSON.stringify({login, password})
    })
        .then(data => {
            localStorage.setItem('token', data.token);
            return data;
        });
}