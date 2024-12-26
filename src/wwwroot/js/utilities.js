window.DownloadPdfFile = (fileName, url) => {
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
}

function typewriterEffect(elementId) {
    const element = document.getElementById(elementId);
    if (!element) return;

    const delay = 120; 
    const fadeDuration = 200; 

    const staticText = "Hi ";
    const emoji = "👋";
    const dynamicText = ", I'm Josh";

    element.innerHTML = `<span>${staticText}</span><span>${emoji}</span><span id="cursor" class="cursor">|</span>`;

    const cursor = document.getElementById('cursor');

    setInterval(() => {
        if (cursor) {
            cursor.style.opacity = cursor.style.opacity === "0" ? "1" : "0";
        }
    }, 500);

    let index = 0;
    const type = () => {
        if (index < dynamicText.length) {
            const span = document.createElement("span");
            span.textContent = dynamicText.charAt(index);
            span.style.opacity = "0"; 
            span.style.transition = `opacity ${fadeDuration}ms ease-in-out`; 
            element.insertBefore(span, cursor); 

            requestAnimationFrame(() => {
                span.style.opacity = "1";
            });

            index++;
            setTimeout(type, delay);
        } else {
            cursor.remove();
        }
    };

    setTimeout(type, delay); 
}
