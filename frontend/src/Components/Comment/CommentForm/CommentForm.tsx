import React, { useState, SetStateAction, useRef, useEffect } from "react";
import { usePostCommentMutation } from "../../../API/comments.ts";
import cl from "./CommentForm.module.css";
import Comment from "../Comment.tsx";
import { Dispatch } from "@reduxjs/toolkit";
interface CommentFormProps {
	setModal: Dispatch<SetStateAction<boolean>>;
	parentId: number | null;
}
const CommentForm: React.FC<CommentFormProps> = ({ setModal, parentId }) => {
	const API_URL = process.env.REACT_APP_API_URL;
	const textareaRef = useRef<HTMLTextAreaElement | null>(null);
	const [content, setContent] = useState("");
	const [username, setUsername] = useState("");
	const [email, setEmail] = useState("");
	const [homepage, setHomepage] = useState("");
	const [file, setFile] = useState<File | null>(null);
	const [captchaText, setCaptchaText] = useState("");
	const [errors, setErrors] = useState<string[]>([]);
	const [clientValidationErrors, setClientValidationErrors] = useState<{
		[key: string]: string;
	}>({});
	const [captchaUrl, setCaptchaUrl] = useState<string>("/captcha");
	const [postComment, { isLoading, isError, error }] = usePostCommentMutation();
	const [page, setPage] = useState<1 | 2>(1);
	const tags = [
		{
			img: "link.png",
			open: "<a href='' title=''>",
			close: "</a>",
		},
		{
			img: "bold.png",
			open: "<strong>",
			close: "</strong>",
		},
		{
			img: "italic.png",
			open: "<i>",
			close: "</i>",
		},
		{
			img: "code.png",
			open: "<code>",
			close: "</code>",
		},
	];

	const insertTag = (openTag: string, closeTag: string) => {
		if (textareaRef.current) {
			const { selectionStart, selectionEnd, value } = textareaRef.current;
			const selectedText = value.substring(selectionStart, selectionEnd);

			if (selectedText) {
				const newText =
					value.substring(0, selectionStart) +
					openTag +
					selectedText +
					closeTag +
					value.substring(selectionEnd);
				setContent(newText);
			} else {
				const newText =
					value.substring(0, selectionStart) +
					openTag +
					closeTag +
					value.substring(selectionEnd);
				setContent(newText);
			}
		}
	};
	const handleSubmit = async () => {
		const formData = new FormData();
		formData.append("content", content);
		formData.append("username", username);
		formData.append("email", email);
		if (parentId !== null) formData.append("parentId", parentId);
		if (homepage) formData.append("homepage", homepage);
		if (file) formData.append("file", file);
		formData.append("captchaText", captchaText);
		await postComment(formData)
			.unwrap()
			.then((res) => {
				console.log("Comment posted:", res);
				setModal(false);
			})
			.catch((err) => {
				if (err.data.errors) setErrors(Object.values(err.data.errors).flat());
				console.error("Failed to post comment:", err);
				setPage(1);
				handleCaptchaRefresh();
			});
	};

	const handleCaptchaRefresh = () => {
		setCaptchaUrl(`/captcha?timestamp=${new Date().getTime()}`);
		setCaptchaText("");
	};

	function validateURL(url) {
		const urlPattern = /^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$/i;
		return urlPattern.test(url);
	}

	function validateEmail(email) {
		const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
		return emailPattern.test(email);
	}

	useEffect(() => {
		if (username && username.length > 50)
			setClientValidationErrors((prev) => ({
				...prev,
				username: "Username is too long (50 characters max)",
			}));
		else setClientValidationErrors((prev) => ({ ...prev, username: "" }));
		if (email && email.length > 50)
			setClientValidationErrors((prev) => ({
				...prev,
				email: "Email is too long (50 characters max)",
			}));
		else if (email && !validateEmail(email))
			setClientValidationErrors((prev) => ({
				...prev,
				email: "Email is invalid",
			}));
		else setClientValidationErrors((prev) => ({ ...prev, email: "" }));
		if (homepage && homepage.length > 50)
			setClientValidationErrors((prev) => ({
				...prev,
				homepage: "Homepage is too long (50 characters max)",
			}));
		else if (homepage && !validateURL(homepage))
			setClientValidationErrors((prev) => ({
				...prev,
				homepage: "Homepage is invalid",
			}));
		else setClientValidationErrors((prev) => ({ ...prev, homepage: "" }));
		if (content && content.length > 1000)
			setClientValidationErrors((prev) => ({
				...prev,
				content: "Content is too long (1000 characters max)",
			}));
		else setClientValidationErrors((prev) => ({ ...prev, content: "" }));
		if (captchaText)
			setClientValidationErrors((prev) => ({ ...prev, captchaText: "" }));
	}, [username, email, homepage, content, captchaText]);

	useEffect(() => {
		handleCaptchaRefresh();
	}, []);

	return (
		<div
			className={[cl.modalBackground, "closeOnClick"].join(" ")}
			onMouseDown={(event) => {
				if (event.target.classList.contains("closeOnClick")) setModal(false);
			}}
		>
			{page === 1 ? (
				<form onSubmit={handleSubmit}>
					<input
						type="text"
						value={username}
						onChange={(e) => setUsername(e.target.value)}
						placeholder="Username"
					/>
					<input
						type="email"
						value={email}
						onChange={(e) => setEmail(e.target.value)}
						placeholder="Email"
					/>
					<input
						type="text"
						value={homepage}
						onChange={(e) => setHomepage(e.target.value)}
						placeholder="Homepage (optional)"
					/>
					<input
						type="file"
						onChange={(e) => setFile(e.target.files ? e.target.files[0] : null)}
					/>
					<div style={{ display: "flex", gap: "5px" }}>
						{tags.map((tag) => (
							<button
								key={tag.img}
								style={{
									padding: "5px",
									borderRadius: "5px",
									minWidth: "35px",
								}}
								onClick={(e) => {
									e.preventDefault();
									insertTag(tag.open, tag.close);
								}}
							>
								<img
									src={process.env.PUBLIC_URL + "/" + tag.img}
									alt={tag.img}
									style={{ width: "24px", height: "24px" }}
								/>
							</button>
						))}
					</div>
					<textarea
						value={content}
						onChange={(e) => setContent(e.target.value)}
						placeholder="Write a comment"
						ref={textareaRef}
						style={{ resize: "none" }}
					/>
					<div style={{ display: "flex", alignItems: "center", gap: "5px" }}>
						<img src={API_URL + captchaUrl} alt="Captcha" />
						<div
							style={{
								display: "flex",
								alignItems: "center",
								gap: "5px",
								flexDirection: "column",
							}}
						>
							<button
								style={{
									padding: "5px",
									borderRadius: "5px",
									minWidth: "35px",
									width: "100%",
								}}
								onClick={(e) => {
									e.preventDefault();
									handleCaptchaRefresh();
								}}
							>
								Update CAPTCHA
							</button>
							<input
								type="text"
								value={captchaText}
								onChange={(e) => setCaptchaText(e.target.value)}
								placeholder="Enter captcha"
							/>
						</div>
					</div>
					<button
						type="submit"
						disabled={
							Object.values(clientValidationErrors)
								.flat()
								.filter((el) => (el as string).length > 0).length > 0
						}
						onClick={(e) => {
							e.preventDefault();
							let validated = true;
							if (!username) {
								setClientValidationErrors((prev) => ({
									...prev,
									username: "Username is required",
								}));
								validated = false;
							}
							if (!email) {
								setClientValidationErrors((prev) => ({
									...prev,
									email: "Email is required",
								}));
								validated = false;
							}
							if (!content) {
								setClientValidationErrors((prev) => ({
									...prev,
									content: "Content is required",
								}));
								validated = false;
							}
							if (!captchaText) {
								setClientValidationErrors((prev) => ({
									...prev,
									captchaText: "Captcha is required",
								}));
								validated = false;
							}
							if (validated) setPage(2);
						}}
					>
						View comment
					</button>

					{errors.map((error, index) => (
						<p key={index} style={{ color: "red", textAlign: "center" }}>
							{error}
						</p>
					))}
					{Object.values(clientValidationErrors)
						.flat()
						.map(
							(error, index) =>
								error && (
									<p key={index} style={{ color: "red", textAlign: "center" }}>
										{error}
									</p>
								)
						)}
				</form>
			) : (
				<div className={cl.commentView}>
					<Comment
						comment={{
							id: -1,
							content: content,
							parentId: parentId,
							childrenCount: 0,
							createdAt: new Date().toISOString().replace("Z", "+00:00"),
							author: { username: username, email: email, homepage: homepage },
							fileURL: null,
						}}
					/>
					<div style={{ display: "flex", gap: "5px", marginTop: "20px" }}>
						{!isLoading && (
							<button
								onClick={(e) => {
									e.preventDefault();
									setPage(1);
								}}
							>
								Back
							</button>
						)}
						<button disabled={isLoading} onClick={handleSubmit}>
							{isLoading ? "Posting..." : "Post Comment"}
						</button>
					</div>
				</div>
			)}
		</div>
	);
};

export default CommentForm;
