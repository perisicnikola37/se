export type Page = {
  name: string;
  url: string;
};

export type Info = {
  applicationName: string;
  copyright: string;
};

export type FooterLink = {
  name: string;
  url: string;
};

export type LanguageConfig = {
  faqData: [];
  acceptButton: string;
  privacyPolicy: string;
  policyText: string;
  mainTextOnDashboard1: string;
  mainTextOnDashboard2: string;
  textOnDashboard: string;
  getStarted: string;
  exploreRepository: string;
  featuredIn: string;
  mailchimpText: string;
  mailchimpHeading1: string;
  mailchimpHeading2: string;
  emailExportText: string;
  emailExportHeading1: string;
  emailExportHeading2: string;
  newsletterHeading: string;
  newsletterText: string;
  subscribedMessage: string;
  emailLabel: string;
  emailPlaceholder: string;
  subscribeButton: string;
  privacyPolicyText: string;
  privacyPolicyLinkText: string;
  pages: Page[];
  info: Info;
  footerLinks: FooterLink[];
  settings: string[];
  testimonials1: string;
  testimonials2: string;
  checkAll: string;
  reviews: string;
  pricing1: string;
  pricing2: string;
  pricing3: string;
  pricing4: string;
  pricing5: string;
  pricing6: string;
  pricing7: string;
  pricing8: string;
  pricing9: string;
  pricing10: string;
  pricing11: string;
  pricing12: string;
  unlimited: string;
  subHeaderMessage: string;
  checkItOut: string;
  signIn: string;
};

export type Config = {
  [key: string]: LanguageConfig;
};
