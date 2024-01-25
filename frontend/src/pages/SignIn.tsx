import logo from "../assets/logo.png"

export default function SignIn() {
    return (
      <div className="flex items-center justify-center h-screen bg-[#222629] text-white">
        <div className="sm:w-full sm:max-w-sm">
          <img
            className="mx-auto w-auto"
            src={logo}
            alt="Your Company"
          />
          <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight ">
            Sign in to your account
          </h2>
  
          <div className="mt-10">
            <form className="space-y-6" action="#" method="POST">
            </form>
          </div>
        </div>
      </div>
    );
  }
  