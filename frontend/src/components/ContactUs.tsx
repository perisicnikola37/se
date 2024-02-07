export default function ContactUs() {
  return (
    <div>
      <section className="text-gray-600 body-font relative w-[100%] lg:w-[250vh]">
        <div className="container px-5 py-24 mx-auto flex sm:flex-nowrap flex-wrap h-[100%] lg:h-[79vh]">
          <div className="lg:w-full md:w-1/2 bg-gray-300 rounded-lg overflow-hidden sm:mr-10 p-10 flex items-end justify-start relative">
            <iframe
              title="map"
              width="100%"
              height="500px"
              className="absolute inset-0"
              src="https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d11777.504729548693!2d19.2438091!3d42.4410115!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x134deb25b4739d11%3A0x5d7394fc13e86668!2sThe%20Capital%20Plaza!5e0!3m2!1sen!2s!4v1707233425743!5m2!1sen!2s"
            ></iframe>
            <div className="bg-white relative flex flex-wrap py-6 rounded shadow-md">
              <div className="lg:w-1/2 px-6">
                <h4 className="title-font font-semibold text-gray-900 tracking-widest text-xs">
                  ADDRESS
                </h4>
                <p className="mt-1 text-sm">
                  The Capital Plaza St. Sheikh Zayed 13, 81000
                </p>
              </div>
              <div className="lg:w-1/2 px-6 mt-4 lg:mt-0 text-sm">
                <h2 className="title-font font-semibold text-gray-900 tracking-widest text-xs">
                  EMAIL
                </h2>
                <a
                  href="mailto:expensetracker@gmail.com"
                  className="text-red-500 leading-relaxed"
                >
                  expensetracker@gmail.com
                </a>
                <h2 className="title-font font-semibold text-gray-900 tracking-widest text-xs mt-4">
                  PHONE
                </h2>
                <p className="leading-relaxed">123-456-7890</p>
              </div>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
}
