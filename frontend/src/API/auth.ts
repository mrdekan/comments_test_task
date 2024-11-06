import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import { LoginRequest, LoginResponse, RegisterRequest } from '../Types/authTypes';
const API_URL = process.env.REACT_APP_API_URL;
export const authApi = createApi({
    reducerPath: 'authApi',
    baseQuery: fetchBaseQuery({
        baseUrl: API_URL+'/auth',
        
    }),
    endpoints: (build) => ({
        login: build.mutation<LoginResponse, LoginRequest>({
            query: (body) => ({
                url: '/login',
                method: 'POST',
                body,
            }),
        }),
        register: build.mutation<any, FormData>({
            query: (formData) => ({
                url: '/register',
                method: 'POST',
                body: formData,
            }),
        }),
        isAuthorized: build.query<any, void>({
            query: () => ({
                url: '/isAuthorized',
                method: 'GET',
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem('token'),
                }
            }),
        }),
        logout: build.mutation<any, void>({
            query: () => ({
                url: '/logout',
                method: 'POST',
                headers: {
                    'Authorization': 'Bearer ' + localStorage.getItem('token'),
                }
            }),
        }),
    }),
});
export const {
    useLoginMutation,
    useRegisterMutation,
    useLazyIsAuthorizedQuery,
    useLogoutMutation,
} = authApi;