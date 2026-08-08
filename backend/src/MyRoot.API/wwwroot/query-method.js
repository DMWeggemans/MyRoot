let queryPaths = [];

function initQueryPaths() {
    if (!window.ui || queryPaths.length)
        return;

    try {
        const specifications = window.ui.specSelectors.specJson().toJS();
        if (!specifications || !specifications.paths)
            return;

        Object.keys(specifications.paths).forEach(key => {
            const data = specifications.paths[key];

            if (!data || !(data['X-QUERY'] || data.get?.['X-QUERY']))
                return;

            const tag = (data['X-QUERY']?.tags || data.get?.['X-QUERY']?.tags)[0];
            const controller = document.querySelector(`div:has(> h3[data-tag="${tag}"])`);
            const endpoints = controller.querySelector('.operation-tag-content');

            endpoints.querySelectorAll('& > span > .opblock').forEach((element, index) => {
                const method = element.querySelector('.opblock-summary-method')?.textContent;
                const path = element.querySelector('.opblock-summary-path span')?.textContent;
                if (method !== 'GET' || path !== key) return;

                queryPaths.push({
                    element: element,
                    path: path
                });
            });
        });
    } catch (error) {
        console.error("Failed to get all query endpoints from Swagger UI!:", error);
    }
}

const originalFetch = window.fetch;
window.fetch = async function (...args) {
    const [resource, config] = args;
    console.log(resource, config);

    if (config && config.method === 'GET' && config.url) {
        try {
            const url = new URL(config.url, window.location.origin);
            const path = url.pathname;

            if (queryPaths.some(x => x.path === path))
                config.method = 'QUERY';
        } catch (error) {
            console.error("Failed to intercept an endpoint from Swagger UI!:", error);
        }
    }

    return originalFetch.apply(this, args);
};

document.addEventListener('DOMContentLoaded', function () {
    const observer = new MutationObserver(() => {
        initQueryPaths();

        queryPaths.forEach(path => {
            if (path.element.classList.contains('opblock-get'))
                path.element.classList.remove('opblock-get');

            if (!path.element.classList.contains('opblock-query'))
                path.element.classList.add('opblock-query');

            const method = path.element.querySelector('.opblock-summary-method');
            if (method && method.textContent !== 'QUERY')
                method.textContent = 'QUERY';

            const curl = path.element.querySelector('.curl');
            if (!curl || curl.textContent.includes("-X 'QUERY'")) return;

            curl.textContent = curl.textContent.replace("-X 'GET'", "-X 'QUERY'");
        });
    });

    observer.observe(document.body, { childList: true, subtree: true });
});