export type Page = {
    name: string;
    url: string;
};

export type LanguageConfig = {
    pages: Page[];
};

export type Config = {
    [key: string]: LanguageConfig;
};