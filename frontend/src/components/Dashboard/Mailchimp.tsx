interface MailChimpProps {
    imageUrl: string;
}

const Mailchimp = ({ imageUrl }: MailChimpProps) => {
    return (
        <div className="m-8 md:m-20 flex flex-col md:flex-row w-[100%] lg:w-[80%] xl:w-[40%] 2xl:w-[60%]">
            <div className="w-full md:w-1/2 flex justify-center items-center">
                <img
                    src={imageUrl}
                    alt="Mailchimp Logo"
                    className="max-w-full h-auto md:max-h-full object-cover hidden md:block"
                    style={{ width: "300px", height: "auto" }}
                />
            </div>
            <div className="w-full md:w-3/4 p-4 md:p-20  md:pl-8  md:ml-8 mt-4 md:mt-0">
                <h1 className="text-2xl md:text-4xl tracking-tighter leading-none font-bold mb-2 md:mb-5">
                    <span className="text-[#2563EB]">Integration</span> with third party
                    services
                </h1>
                <p className="text-gray-500 mt-5 lg:mt-0">
                    Enhance your business capabilities with seamless integration of
                    third-party services. Our platform empowers you to effortlessly
                    connect and collaborate with a variety of external tools and
                    applications. Streamline your workflow, improve efficiency, and unlock
                    new possibilities for growth. Discover the convenience of integrating
                    with industry-leading services, all within the reach of your
                    dashboard.
                </p>
            </div>
        </div>
    );
};

export default Mailchimp;
