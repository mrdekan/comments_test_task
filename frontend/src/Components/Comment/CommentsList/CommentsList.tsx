import React, { useEffect } from "react";

import Comment from "../Comment.tsx";
interface CommentsListProps {
	comments: { [key: string]: Comment[] };
	handleResponse: (id: number) => void;
	setOpenCommentStatus: (
		id: number,
		status: boolean,
		count: number
	) => Promise<void>;
	id: number | null;
	getCommentsByParentId: (parentId: number | null) => Comment[];
}
const CommentsList: React.FC<CommentsListProps> = ({
	comments,
	handleResponse,
	setOpenCommentStatus,
	id,
	getCommentsByParentId,
}) => {
	console.log(comments, id, getCommentsByParentId(id));
	return (
		<div
			style={{ display: "flex", flexDirection: "column", alignItems: "end" }}
		>
			{getCommentsByParentId(id).map((comment) => (
				<Comment
					key={comment.id}
					comment={comment}
					handleResponse={handleResponse}
					setOpenCommentStatus={setOpenCommentStatus}
					getCommentsByParentId={getCommentsByParentId}
				/>
			))}
		</div>
	);
};

export default CommentsList;
