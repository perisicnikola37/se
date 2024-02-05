import { useState, useEffect } from "react";
import useMailchimpSubscribe from "../hooks/ThirdPartyServices/MailchimpSubscribeHook";
import { validateEmail } from "../utils/utils";

const Newsletter = () => {
    const { subscribeToMailchimp } = useMailchimpSubscribe();
    const [isSubscribed, setIsSubscribed] = useState(false);
    const [email, setEmail] = useState("");
    const [isEmailValid, setIsEmailValid] = useState(true);

    useEffect(() => {
        const storedSubscriptionStatus = localStorage.getItem("isSubscribed");
        if (storedSubscriptionStatus === "true") {
            setIsSubscribed(true);
        }
    }, []);

    const toggleAlert = () => {
        if (validateEmail(email)) {
            subscribeToMailchimp(email);
            localStorage.setItem("isSubscribed", "true");
            setIsSubscribed(true);
        } else {
            setIsEmailValid(false);
        }
    };

    return (
        <section className="bg-white text-black mb-10 lg:mb-0">
            <div className="py-8 px-4 mx-auto max-w-screen-xl lg:py-16 lg:px-6">
                <div className="mx-auto max-w-screen-md sm:text-center">
                    <h2 className="mb-4 text-3xl tracking-tight font-extrabold text-gray-900 sm:text-4xl">
                        Sign up for our newsletter
                    </h2>
                    <p className="mx-auto mb-8 max-w-2xl font-light text-gray-500 md:mb-12 sm:text-xl">
                        Stay up to date with the roadmap progress, announcements, and
                        exclusive discounts. Feel free to sign up with your email.
                    </p>
                    {isSubscribed ? (
                        <p className="text-[#4F65EB] font-bold">Subscribed :)</p>
                    ) : (
                        <form>
                            <div className="flex items-center mx-auto mb-3 space-y-4 max-w-screen-sm sm:flex sm:space-y-0">
                                <div className="relative w-full">
                                    <label
                                        htmlFor="email"
                                        className="hidden mb-2 text-sm font-medium text-gray-900"
                                    >
                                        Email address
                                    </label>
                                    <div className="flex absolute inset-y-0 left-0 items-center pl-3 pointer-events-none">
                                        <svg
                                            className="w-5 h-5 text-gray-500"
                                            fill="currentColor"
                                            viewBox="0 0 20 20"
                                            xmlns="http://www.w3.org/2000/svg"
                                        ></svg>
                                    </div>
                                    <input
                                        className={`block p-3 lg:pl-10 sm:pl-3 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300 sm:rounded-none sm:rounded-l-lg outline-none ${isEmailValid ? "" : "border-red-500"
                                            }`}
                                        placeholder="Enter your email"
                                        type="email"
                                        id="email"
                                        value={email}
                                        onChange={(e) => {
                                            setEmail(e.target.value);
                                            setIsEmailValid(true);
                                        }}
                                        required
                                    />
                                </div>
                                <div className="hidden sm:block">
                                    <button
                                        onClick={toggleAlert}
                                        className="py-3 px-5 w-full text-sm font-medium text-center text-white rounded-lg border cursor-pointer bg-[#2563EB] border-primary-600 sm:rounded-none sm:rounded-r-lg hover:bg-[#3463c9] duration-200"
                                    >
                                        Subscribe
                                    </button>
                                </div>
                            </div>
                            <div className="mx-auto max-w-screen-sm text-sm text-left text-gray-500 newsletter-form-footer">
                                We care about the protection of your data.{" "}
                                <a
                                    href="#"
                                    className="font-medium text-primary-600 hover:underline"
                                >
                                    Read our Privacy Policy
                                </a>
                            </div>
                        </form>
                    )}
                </div>
            </div>
            {!isSubscribed && (
                <div className="mx-auto mb-5 text-center max-w-screen-sm sm:hidden">
                    <button
                        onClick={toggleAlert}
                        className="py-3 px-5 w-[60%] text-sm font-medium text-center text-white rounded-lg border cursor-pointer bg-[#2563EB] border-primary-600 hover:bg-[#3463c9] duration-200"
                    >
                        Subscribe
                    </button>
                </div>
            )}
        </section>
    );
};

export default Newsletter;
