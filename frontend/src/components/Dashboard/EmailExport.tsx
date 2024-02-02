interface EmailExportProps {
    imageUrl: string;
}

const EmailExport: React.FC<EmailExportProps> = ({ imageUrl }) => {
    return (
        <div className="m-8 md:m-20 flex flex-col md:flex-row w-[60%]">
            <div className="w-full md:w-1/2 flex justify-center items-center">
                <img
                    src={imageUrl}
                    alt="Mailchimp Logo"
                    className="max-w-full h-auto md:max-h-full object-cover"
                    style={{ width: "300px", height: "auto" }}
                />
            </div>
            <div className="w-full md:w-3/4 p-4 md:p-20 pl-4 md:pl-8 ml-4 md:ml-8 mt-4 md:mt-0">
                <h1 className="text-2xl md:text-4xl tracking-tighter leading-none font-bold mb-2 md:mb-5">
                    <span className="text-[#2563EB]">Export</span> expenses & incomes
                    easily
                </h1>
                <p className="text-gray-500">
                    Exporting expenses and incomes is a breeze with our user-friendly
                    platform, offering seamless financial management. Effortlessly
                    streamline your financial processes by utilizing our intuitive tools
                    for quick and efficient data export. Take control of your financial
                    data with just a few clicks, ensuring a hassle-free experience in
                    tracking both expenses and incomes. Our platform empowers you to
                    simplify your financial workflows, providing a comprehensive solution
                    for easy and effective exportation. Experience the convenience and
                    efficiency of exporting expenses and incomes, making informed
                    financial decisions has never been more accessible.
                </p>
            </div>
        </div>
    );
};

export default EmailExport;