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
export interface GetTopLayerCommentsResponse extends GetCommentsResponse{
  totalPages: number;
  commentsPerPage: number;
}

export interface GetTopLevelCommentsRequest{
  page: number;
  sorting: string;
}