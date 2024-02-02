const FeaturedInSection = () => {
    return (
        <div className="_featured-in max-w-4xl mt-40 mx-auto text-center select-none">
            <h2 className="text-xl font-semibold -tracking-tight mb-4 text-gray-500 uppercase">
                Featured in
            </h2>

            <div className="flex flex-col items-center space-y-4 md:flex-row md:space-y-0 md:space-x-4">
                <img
                    width={100}
                    src="https://www.globalemancipation.ngo/wp-content/uploads/2017/09/github-logo.png"
                    alt="GitHub"
                    title="GitHub"
                    className="max-w-xs h-auto filter grayscale hover:filter-none transition-all duration-300"
                />

                <img
                    width={130}
                    src="https://images.ctfassets.net/xz1dnu24egyd/4QGmokIyrhHxpfmYIKHq17/ef43a9f88f2a9c1da8f5382383756d46/gitlab-logo-150.jpg"
                    alt="GitLab"
                    title="GitLab"
                    className="max-w-xs h-auto filter grayscale hover:filter-none hover:brightness-110 transition-all duration-300"
                />

                <img
                    width={80}
                    src="https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/LinkedIn_Logo.svg/2560px-LinkedIn_Logo.svg.png"
                    alt="LinkedIn"
                    title="LinkedIn"
                    className="max-w-xs h-auto filter grayscale hover:filter-none hover:brightness-110 transition-all duration-300 mr-4"
                />

                <img
                    width={80}
                    src="https://1000logos.net/wp-content/uploads/2016/11/Facebook-Logo-2019.png"
                    alt="Facebook"
                    title="Facebook"
                    className="max-w-xs h-auto filter grayscale hover:filter-none hover:brightness-110 transition-all duration-300"
                />
                <img
                    width={60}
                    src="https://upload.wikimedia.org/wikipedia/commons/thumb/e/e1/Logo_of_YouTube_%282015-2017%29.svg/2560px-Logo_of_YouTube_%282015-2017%29.svg.png"
                    alt="YouTube"
                    title="YouTube"
                    className="max-w-xs h-auto filter grayscale hover:filter-none hover:brightness-110 transition-all duration-300"
                />
            </div>
        </div>
    );
};

export default FeaturedInSection;