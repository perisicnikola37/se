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
    pages: Page[];
    info: Info;
    footerLinks: FooterLink[];
    settings: string[];
};

export type Config = {
    [key: string]: LanguageConfig;
};
