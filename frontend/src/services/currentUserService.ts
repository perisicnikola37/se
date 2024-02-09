import axiosConfig from "../config/axiosConfig";
import { User } from "../interfaces/globalInterfaces";

const fetchCurrentUser = async () => {
    const result = {
        isLoading: true,
        user: null as User | null,
        error: null as string | null,
    };

    try {
        const response = await axiosConfig.get("/api/auth/user");
        result.user = response.data;
    } catch (err) {
        result.error = "Error fetching current user.";
    } finally {
        result.isLoading = false;
    }

    return result;
};

export default fetchCurrentUser;
