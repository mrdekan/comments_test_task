export interface LoginRequest {
    email: string;
    password: string;
}
export interface RegisterRequest {
    password: string;
    name:string;
    surname:string;
    username:string;
    avatar:File;
}
export interface LoginResponse {
    token: string;
}