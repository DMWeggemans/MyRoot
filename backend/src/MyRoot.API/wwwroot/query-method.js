let storage = [];

(function () {
    // --- 1. EXTRACT QUERY ENDPOINTS ONCE ---
    function initializeQueryEndpointsCache() {
        if (!window.ui) return;

        try {
            const fullSpec = window.ui.specSelectors.specJson().toJS();
            const queryPaths = [];

            if (fullSpec && fullSpec.paths) {
                Object.keys(fullSpec.paths).forEach(pathKey => {
                    const pathData = fullSpec.paths[pathKey];
                    // Match either the path-level extension or the fallback GET method extension
                    const hasQueryExtension = pathData && (pathData['X-QUERY'] || pathData.get?.['X-QUERY']);

                    if (hasQueryExtension) {
                        // Store the path normalized to lowercase
                        queryPaths.push(pathKey.toLowerCase());
                    }
                });
            }

            if (queryPaths.length > 0) {
                storage = queryPaths;
            }
        } catch (e) {
            console.error("Failed to parse and store custom query routes into cache:", e);
        }
    }

    // Helper to check if a specific path is registered as a QUERY method
    function isQueryMethodRoute(path) {
        return storage.includes(path.toLowerCase());
    }

    // --- 2. GLOBAL NETWORK INTERCEPTOR (FETCH PROXY) ---
    const originalFetch = window.fetch;
    window.fetch = async function (...args) {
        let [resource, config] = args;

        if (config && config.method === 'GET') {
            const urlString = typeof resource === 'string' ? resource : resource.url;

            if (urlString) {
                try {
                    const urlObj = new URL(urlString, window.location.origin);
                    const pathText = urlObj.pathname;

                    // Instantly look up the path against the flat session storage collection
                    if (isQueryMethodRoute(pathText)) {
                        config.method = 'QUERY'; // Enforce actual HTTP verb payload over the wire
                    }
                } catch (e) {
                    console.error("Fetch intercept error:", e);
                }
            }
        }
        return originalFetch.apply(this, args);
    };

    // --- 3. DYNAMIC LAYOUT OBSERVATION & VISUAL REWRITE ---
    document.addEventListener('DOMContentLoaded', function () {
        const observer = new MutationObserver(() => {
            // Trigger specification caching as soon as Swagger UI is ready
            if (window.ui) {
                initializeQueryEndpointsCache();
            }

            const pathItems = document.querySelectorAll('.opblock-summary');
            pathItems.forEach(summary => {
                const pathText = summary.querySelector('.opblock-summary-path')?.getAttribute('data-path');
                const methodBadge = summary.querySelector('.opblock-summary-method');

                if (pathText && methodBadge && isQueryMethodRoute(pathText)) {
                    // Force display badge text conversion
                    if (methodBadge.textContent !== 'QUERY') {
                        methodBadge.textContent = 'QUERY';
                    }

                    // Dynamically manage visual design classes
                    const mainContainer = summary.closest('.opblock');
                    if (mainContainer) {
                        if (mainContainer.classList.contains('opblock-get')) {
                            mainContainer.classList.remove('opblock-get');
                        }
                        if (!mainContainer.classList.contains('opblock-query')) {
                            mainContainer.classList.add('opblock-query');
                        }

                        // Modify the cURL text command preview block
                        const curlBlock = mainContainer.querySelector('.curl');
                        if (curlBlock && curlBlock.textContent.includes("-X 'GET'")) {
                            curlBlock.textContent = curlBlock.textContent.replace("-X 'GET'", "-X 'QUERY'");
                        }
                    }
                }
            });
        });

        observer.observe(document.body, { childList: true, subtree: true });
    });
})();
