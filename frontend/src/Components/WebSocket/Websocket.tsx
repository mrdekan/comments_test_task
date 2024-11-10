import React, { useEffect, useState, Promise, useRef } from "react";
import CommentForm from "../Comment/CommentForm/CommentForm.tsx";
import CommentsList from "../Comment/CommentsList/CommentsList.tsx";
import {
	useGetTopLayerCommentsQuery,
	useLazyGetChildrenQuery,
} from "../../API/comments.ts";
import { Comment } from "../../Types/comments.ts";
import FileViewer from "../FileViewer/FileViewer.tsx";

const Websocket: React.FC = () => {
	const WS_URL = process.env.REACT_APP_WS_URL;
	const [page, setPage] = useState<number>(1);
	const [totalPages, setTotalPages] = useState<number>(0);
	const [modal, setModal] = useState<boolean>(false);
	const [fileToView, setFileToView] = useState<string | null>(null);
	const [openComments, setOpenComments] = useState<number[]>([]);
	const [responseId, setResponseId] = useState<number | null>(null);
	const [socket, setSocket] = useState<WebSocket | null>(null);
	const [comments, setComments] = useState<{ [key: string]: Comment[] }>({});
	const [getChildrenAsync] = useLazyGetChildrenQuery();
	const sortingFields = ["Date", "Username", "Email"];
	const [sortingField, setSortingField] = useState<
		"Date" | "Username" | "Email"
	>("Date");
	const [sortingDirection, setSortingDirection] = useState<"Asc" | "Desc">(
		"Desc"
	);
	const sort = useRef<string>("dateDesc");
	const perPage = useRef<number>(25);
	const { data: topLayerComments, refetch } = useGetTopLayerCommentsQuery({
		page: page,
		sorting: sort.current,
	});
	const sortingDirections = [
		{ name: "Descending", code: "Desc" },
		{ name: "Ascending", code: "Asc" },
	];

	function addComments(commentsArr: Comment[], newComment: boolean) {
		setComments((prevState) => {
			let updatedComments = { ...prevState };

			commentsArr.forEach((comment) => {
				const key =
					comment.parentId === null ? "_null_" : comment.parentId.toString();

				if (updatedComments[key]) {
					if (
						updatedComments[key].findIndex((obj) => obj.id === comment.id) ===
						-1
					) {
						if (newComment && comment.parentId === null) {
							let newArr = insertAndMaintainArray(
								updatedComments[key],
								comment,
								sort.current
							);
							updatedComments[key] = newArr;
						} else {
							updatedComments[key].push(comment);
						}
					}
				} else {
					updatedComments[key] = [comment];
				}

				if (newComment && comment.parentId !== null) {
					for (const k in updatedComments) {
						let objIndex = updatedComments[k].findIndex(
							(obj) => obj.id === comment.parentId
						);
						if (objIndex !== -1) {
							let obj = updatedComments[k][objIndex];

							const updatedObj = {
								...obj,
								childrenCount: obj.childrenCount + 1,
							};

							updatedComments[k] = [
								...updatedComments[k].slice(0, objIndex),
								updatedObj,
								...updatedComments[k].slice(objIndex + 1),
							];
						}
					}
				}
			});

			return updatedComments;
		});
	}

	async function getChildren(id: number, count: number): Promise<void> {
		let res = getCommentsByParentId(id);
		if (res.length < count) {
			let data = await getChildrenAsync(id);
			if (data && data.data?.comments) res = data.data.comments;
			addComments(res, false);
		}
	}

	const getCommentsByParentId = (parentId: number | null): Comment[] => {
		const key = parentId === null ? "_null_" : parentId.toString();

		return comments[key] || [];
	};

	function responseHandler(id: number) {
		setResponseId(id);
		setModal(true);
	}

	async function setOpenCommentStatus(
		id: number,
		status: boolean,
		count: number
	): Promise<void> {
		console.log(id, status, count);
		await getChildren(id, count);
		if (status) {
			setOpenComments([...openComments, id]);
		} else {
			setOpenComments(openComments.filter((commentId) => commentId !== id));
		}
	}

	function insertAndMaintainArray(
		arr: Comment[],
		newItem: Comment,
		sortType: string
	) {
		const comparator: { [key: string]: (a: any, b: any) => number } = {
			usernameAsc: (a, b) => {
				const result = a.author.username.localeCompare(b.author.username);
				if (result === 0) {
					return (
						new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
					);
				}
				return result;
			},
			usernameDesc: (a, b) => {
				const result = b.author.username.localeCompare(a.author.username);
				if (result === 0) {
					return (
						new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
					);
				}
				return result;
			},
			emailAsc: (a, b) => {
				const result = a.author.email.localeCompare(b.author.email);
				if (result === 0) {
					return (
						new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
					);
				}
				return result;
			},
			emailDesc: (a, b) => {
				const result = b.author.email.localeCompare(a.author.email);
				if (result === 0) {
					return (
						new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
					);
				}
				return result;
			},
			dateAsc: (a, b) =>
				new Date(a.createdAt).getTime() - new Date(b.createdAt).getTime(),
			dateDesc: (a, b) =>
				new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime(),
		};

		if (!comparator[sortType]) {
			throw new Error(`Unsupported sortType: ${sortType}`);
		}
		let indexToInsert = arr.findIndex(
			(item) => comparator[sortType](newItem, item) < 0
		);
		if (indexToInsert === -1) {
			return arr;
		}

		arr.splice(indexToInsert, 0, newItem);
		if (perPage.current < arr.length) arr.pop();

		return arr;
	}

	useEffect(() => {
		refetch();
	}, [page, sortingField, sortingDirection]);

	useEffect(() => {
		sort.current = (sortingField as string).toLowerCase() + sortingDirection;
	}, [sortingField, sortingDirection]);

	useEffect(() => {
		console.log(topLayerComments);
		if (topLayerComments && topLayerComments.comments) {
			addComments(topLayerComments.comments, false);
			setTotalPages(topLayerComments.totalPages);
			perPage.current = topLayerComments.commentsPerPage;
		}
	}, [topLayerComments]);

	useEffect(() => {
		const ws = new WebSocket(WS_URL as string);

		ws.onopen = () => {
			console.log("Connected to WebSocket");
		};

		ws.onmessage = (event) => {
			let commentTmp = JSON.parse(event.data);
			let comment: Comment = {
				author: {
					email: commentTmp.Author.Email,
					username: commentTmp.Author.Username,
					homepage: commentTmp.Author.Homepage,
				},
				content: commentTmp.Content,
				fileURL: commentTmp.FileURL,
				createdAt: commentTmp.CreatedAt,
				id: commentTmp.Id,
				childrenCount: commentTmp.ChildrenCount,
				parentId: commentTmp.ParentId,
			};
			console.log(comment);
			addComments([comment], true);
		};

		ws.onerror = (error) => {
			console.error("WebSocket error:", error);
		};

		ws.onclose = () => {
			console.log("Disconnected from WebSocket");
		};

		setSocket(ws);

		return () => {
			ws.close();
		};
	}, []);

	return (
		<div style={{ width: "75vw", minWidth: "700px", margin: "auto" }}>
			<button
				onClick={() => {
					setResponseId(null);
					setModal(true);
				}}
				style={{
					marginBottom: "20px",
					color: "white",
					background: "gray",
					padding: "10px",
					borderRadius: "8px",
					marginTop: "20px",
					fontSize: "16px",
				}}
			>
				Write a comment
			</button>
			<div
				style={{
					marginBottom: "20px",
					display: "flex",
					gap: "5px",
					justifyContent: "center",
				}}
			>
				{Array.from({ length: totalPages }, (_, i) => (
					<button
						key={i + 1}
						onClick={() => {
							if (i + 1 === page) return;
							setComments({});
							setPage(i + 1);
						}}
						style={{
							padding: "10px",
							borderRadius: "8px",
							background: i + 1 === page ? "gray" : "white",
							color: i + 1 === page ? "white" : "black",
							width: "39px",
						}}
					>
						{i + 1}
					</button>
				))}
			</div>
			<div
				style={{
					marginBottom: "20px",
					display: "flex",
					gap: "5px",
					justifyContent: "center",
					alignItems: "center",
				}}
			>
				<p
					style={{
						height: "18px",
						lineHeight: "18px",
						fontSize: "18px",
					}}
				>
					Sort by:
				</p>
				<div
					style={{
						display: "flex",
						gap: "5px",
						justifyContent: "center",
					}}
				>
					{sortingFields.map((field, i) => (
						<button
							key={i + 1}
							onClick={() => {
								if (field === sortingField) return;
								setComments({});
								setSortingField(field);
							}}
							style={{
								padding: "10px",
								borderRadius: "8px",
								background: field === sortingField ? "gray" : "white",
								color: field === sortingField ? "white" : "black",
								fontSize: "18px",
							}}
						>
							{field}
						</button>
					))}
				</div>
			</div>
			<div
				style={{
					marginBottom: "20px",
					display: "flex",
					gap: "5px",
					justifyContent: "center",
					alignItems: "center",
				}}
			>
				<p
					style={{
						height: "18px",
						lineHeight: "18px",
						fontSize: "18px",
					}}
				>
					Sort direction:
				</p>
				<div
					style={{
						display: "flex",
						gap: "5px",
						justifyContent: "center",
					}}
				>
					{sortingDirections.map((dir, i) => (
						<button
							key={i + 1}
							onClick={() => {
								if (dir.code === sortingDirection) return;
								setComments({});
								setSortingDirection(dir.code);
							}}
							style={{
								padding: "10px",
								borderRadius: "8px",
								background: dir.code === sortingDirection ? "gray" : "white",
								color: dir.code === sortingDirection ? "white" : "black",
								fontSize: "18px",
							}}
						>
							{dir.name}
						</button>
					))}
				</div>
			</div>
			<CommentsList
				handleResponse={responseHandler}
				setOpenCommentStatus={setOpenCommentStatus}
				id={null}
				getCommentsByParentId={getCommentsByParentId}
				setFileToView={setFileToView}
			/>
			{modal && <CommentForm parentId={responseId} setModal={setModal} />}
			{fileToView && (
				<FileViewer setOpen={setFileToView} fileUrl={fileToView} />
			)}
		</div>
	);
};

export default Websocket;
