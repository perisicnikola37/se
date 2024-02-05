import { Helmet } from 'react-helmet';
import userPicture from "../../src/assets/profile_image.jpg";
import { useEffect } from 'react';
import useCurrentUser from '../hooks/Authentication/GetUserHook';
import useDeleteAccount from '../hooks/Authentication/DeleteAccountHook';

const Profile = () => {
    const { loadCurrentUser, user } = useCurrentUser()
    const { deleteAccount } = useDeleteAccount()

    useEffect(() => {
        loadCurrentUser();
    }, []);

    const handleDeleteAccount = () => {
        deleteAccount()
    }

    const totalIncome = user?.incomes.reduce((total, income) => total + income.amount, 0) || 0;

    return (
        <div className="w-full max-w-screen-xl min-h-[48rem] mx-auto p-4 md:py-8">

            <Helmet>
                <title>User profile | Expense Tracker</title>
            </Helmet>

            <div className="p-16">
                <div className="p-8 bg-white shadow mt-24">
                    <div className="grid grid-cols-1 md:grid-cols-3">
                        <div className="grid grid-cols-3 text-center order-last md:order-first mt-20 md:mt-0">
                            <div>
                                <p className="font-bold text-gray-700 text-xl">{user?.expenses.length}</p>
                                <p className="text-gray-400">Expenses</p>
                            </div>
                            <div>
                                <p className="font-bold text-gray-700 text-xl">{user?.incomes.length}</p>
                                <p className="text-gray-400">Incomes</p>
                            </div>
                            <div>
                                <p className="font-bold text-gray-700 text-xl">${totalIncome}</p>
                                <p className="text-gray-400">Total revenue</p>
                            </div>
                        </div>

                        <div className="relative">
                            <div className="w-48 h-48 mx-auto absolute inset-x-0 top-0 -mt-24 overflow-hidden rounded-full shadow-2xl flex items-center justify-center">
                                <img
                                    src={userPicture}
                                    alt="User profile picture"
                                    className="w-full h-full object-cover rounded-full"
                                />
                            </div>
                        </div>

                        <div className="space-x-8 flex justify-between mt-32 md:mt-0 md:justify-center">
                            <button onClick={handleDeleteAccount} className="text-white py-2 px-4 uppercase rounded bg-red-500 hover:bg-red-600 shadow hover:shadow-lg font-medium transition transform">
                                Delete account
                            </button>
                        </div>
                    </div>

                    <div className="mt-20 text-center border-b pb-12">
                        <h1 className="text-4xl font-medium text-gray-700 mb-3">
                            {user?.username}
                        </h1>
                        <a className='hover:text-[#3363EB] duration-300' href={`mailto:${user?.email}`}>{user?.email}</a>
                        <p className="font-light text-gray-600 mt-3">Podgorica, Montenegro</p>
                        <p className="mt-8 text-gray-500">Solution Manager - Creative Tim Officer</p>
                        <p className="mt-2 text-gray-500">University of Computer Science</p>
                    </div>
                </div>
            </div>
        </div >
    );
};

export default Profile;
