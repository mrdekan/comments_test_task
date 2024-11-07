export interface Comment {
    id: number;
    childrenCount: number;
    content: string;
    fileURL: string|null;
    parentId: number|null;
    createdAt: string;
    author: {
      email: string;
      username: string;
      homepage: string|null;
    }
}
export interface GetCommentsResponse{
    comments: Comment[];
}