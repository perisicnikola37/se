const FeaturedInSection = () => {
    return (
        <div className="max-w-4xl mb-10 mx-auto text-center select-none">
            <h2 className="text-xs font-semibold mb-4 text-gray-500 uppercase">Featured in</h2>

            <div className="flex justify-center items-center space-x-4">
                <img
                    width={100}
                    src="https://assets.stickpng.com/images/629b7adc7c5cd817694c3231.png"
                    alt="Company 1"
                    className="max-w-xs h-auto filter grayscale hover:filter-none transition-all duration-300"
                />

                <img
                    width={130}
                    src="https://images.ctfassets.net/xz1dnu24egyd/4QGmokIyrhHxpfmYIKHq17/ef43a9f88f2a9c1da8f5382383756d46/gitlab-logo-150.jpg"
                    alt="Company 2"
                    className="max-w-xs h-auto filter grayscale hover:filter-none hover:brightness-110 transition-all duration-300"
                />

                <img
                    width={80}
                    src="https://miro.medium.com/v2/resize:fit:8978/1*s986xIGqhfsN8U--09_AdA.png"
                    alt="Company 3"
                    className="max-w-xs h-auto filter grayscale hover:filter-none hover:brightness-110 transition-all duration-300 mr-4"
                />

                <img
                    width={60}
                    src="https://upload.wikimedia.org/wikipedia/commons/thumb/e/e1/Logo_of_YouTube_%282015-2017%29.svg/2560px-Logo_of_YouTube_%282015-2017%29.svg.png"
                    alt="Company 4"
                    className="max-w-xs h-auto filter grayscale hover:filter-none hover:brightness-110 transition-all duration-300"
                />
            </div>
        </div>
    );
};

export default FeaturedInSection;
