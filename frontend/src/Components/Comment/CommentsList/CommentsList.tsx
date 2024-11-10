import React, { Dispatch, SetStateAction } from "react";

import Comment from "../Comment.tsx";
interface CommentsListProps {
	handleResponse: (id: number) => void;
	setOpenCommentStatus: (
		id: number,
		status: boolean,
		count: number
	) => Promise<void>;
	id: number | null;
	getCommentsByParentId: (parentId: number | null) => Comment[];
	setFileToView: Dispatch<SetStateAction<string | null>>;
}
const CommentsList: React.FC<CommentsListProps> = ({
	handleResponse,
	setOpenCommentStatus,
	id,
	getCommentsByParentId,
	setFileToView,
}) => {
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
					setFileToView={setFileToView}
				/>
			))}
		</div>
	);
};

export default CommentsList;
