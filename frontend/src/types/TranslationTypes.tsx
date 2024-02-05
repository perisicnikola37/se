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
    pages: Page[];
    info: Info;
    footerLinks: FooterLink[];
    settings: string[];
};

export type Config = {
    [key: string]: LanguageConfig;
};
