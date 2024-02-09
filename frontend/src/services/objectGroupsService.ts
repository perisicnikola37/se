import axiosConfig from "../config/axiosConfig";
import { ObjectGroupInterface } from "../interfaces/globalInterfaces";

const fetchObjectGroups = async (objectType: string) => {
    const result = {
        objectGroups: [] as ObjectGroupInterface[],
        isLoading: true,
        error: null as string | null,
    };

    try {
        const endpoint = `/api/${objectType}s/groups`;
        const response = await axiosConfig.get<ObjectGroupInterface[]>(endpoint);
        result.objectGroups = response.data;
    } catch (err) {
        result.error = `An error occurred while fetching ${objectType} groups.`;
    } finally {
        result.isLoading = false;
    }

    return result;
};

export default fetchObjectGroups;
