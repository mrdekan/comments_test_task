import React, { useState } from "react";
import { RegisterRequest } from "../../../Types/authTypes.ts";
import { useRegisterMutation } from "../../../API/auth.ts";

const Register = () => {
	const [register, { isLoading, error }] = useRegisterMutation();
	const [formData, setFormData] = useState<RegisterRequest>({
		password: "",
		name: "",
		surname: "",
		username: "",
		avatar: null,
	});

	const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
		const file = e.target.files?.[0] || null;
		setFormData((prevData) => ({
			...prevData,
			avatar: file,
		}));
	};

	const handleSubmit = async (e: React.FormEvent) => {
		e.preventDefault();
		const formToSend = new FormData();
		formToSend.append("password", formData.password);
		formToSend.append("name", formData.name);
		formToSend.append("surname", formData.surname);
		formToSend.append("username", formData.username);
		if (formData.avatar) {
			formToSend.append("avatar", formData.avatar);
		}

		try {
			await register(formToSend).unwrap();
			alert("Registration successful!");
		} catch (err) {
			alert("Registration failed");
		}
	};

	return (
		<form onSubmit={handleSubmit}>
			<input
				type="text"
				value={formData.name}
				onChange={(e) => setFormData({ ...formData, name: e.target.value })}
			/>
			<input
				type="text"
				value={formData.surname}
				onChange={(e) => setFormData({ ...formData, surname: e.target.value })}
			/>
			<input
				type="text"
				value={formData.username}
				onChange={(e) => setFormData({ ...formData, username: e.target.value })}
			/>
			<input
				type="password"
				value={formData.password}
				onChange={(e) => setFormData({ ...formData, password: e.target.value })}
			/>
			<input type="file" onChange={handleFileChange} />

			<button type="submit" disabled={isLoading}>
				{isLoading ? "Submitting..." : "Register"}
			</button>

			{error && (
				<p style={{ color: "red" }}>Registration failed. Please try again.</p>
			)}
		</form>
	);
};

export default Register;
