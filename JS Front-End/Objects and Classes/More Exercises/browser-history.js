function solve(data, commands) {
    function processCommand(command) {
        if (command === "Clear History and Cache") {
            data["Open Tabs"] = [];
            data["Recently Closed"] = [];
            data["Browser Logs"] = [];
        } else {
            const [action, ...rest] = command.split(" ");
            const site = rest.join(" ");

            if (action === "Open") {
                data["Open Tabs"].push(site);
                data["Browser Logs"].push(command);
            } else if (action === "Close") {
                const index = data["Open Tabs"].indexOf(site);
                if (index === -1) return;

                data["Open Tabs"].splice(index, 1);
                data["Recently Closed"].push(site);
                data["Browser Logs"].push(command);
            }
        }
    }

    for (const command of commands) processCommand(command);

    console.log(data["Browser Name"]);
    console.log(`Open Tabs: ${data["Open Tabs"].join(", ")}`);
    console.log(`Recently Closed: ${data["Recently Closed"].join(", ")}`);
    console.log(`Browser Logs: ${data["Browser Logs"].join(", ")}`);
}

solve({ "Browser Name": "Google Chrome", "Open Tabs": ["Facebook", "YouTube", "Google Translate"], "Recently Closed": ["Yahoo", "Gmail"], "Browser Logs": ["Open YouTube", "Open Yahoo", "Open Google Translate", "Close Yahoo", "Open Gmail", "Close Gmail", "Open Facebook"] }, ["Close Facebook", "Open StackOverFlow", "Open Google"]);
solve({ "Browser Name": "Mozilla Firefox", "Open Tabs": ["YouTube"], "Recently Closed": ["Gmail", "Dropbox"], "Browser Logs": ["Open Gmail", "Close Gmail", "Open Dropbox", "Open YouTube", "Close Dropbox"] }, ["Open Wikipedia", "Clear History and Cache", "Open Twitter"]);
