export interface AuthorizedValues {
    id: string;
    expiresOn: Date;
}

// Requests
export interface LoginUserRequest {
    user: string;
    password: string;
}

export interface RegisterUserRequest {
    userName: string;
    email: string;
    password: string;
}