export type AuthorizedValues = {
    id: string;
    expiresOn: Date;
}

// Requests
export type LoginUserRequest = {
    user: string;
    password: string;
}

export type RegisterUserRequest = {
    userName: string;
    email: string;
    password: string;
}