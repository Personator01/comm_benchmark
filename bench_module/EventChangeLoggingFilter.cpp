#include "EventChangeLoggingFilter.h"
#include "Environment.h"
#include "BCIStream.h"
#include <exception>
#include <sstream>

EventChangeLoggingFilter::EventChangeLoggingFilter() {
}

void EventChangeLoggingFilter::Publish() {
    BEGIN_PARAMETER_DEFINITIONS
        "Processing:Event%20Change%20Logging string OutFile= ../data/TimeLogFile % % %"
        "Processing:Event%20Change%20Logging string EventsToLog= % % % %"
    END_PARAMETER_DEFINITIONS
}


std::vector<std::string> split_string(std::string in);

void EventChangeLoggingFilter::Preflight(const SignalProperties& in, SignalProperties& out) const {
    std::vector<std::string> logging_evs = split_string(Parameter("EventsToLog"));
    for (const auto& ev_name : logging_evs) {
        State( ev_name );
    }
}

void EventChangeLoggingFilter::Initialize(const SignalProperties& in, SignalProperties& out) {
    std::string out_path = Parameter("OutFile");
    try {
      out_file.open(out_path);
    } catch (const std::exception& e) {
        bcierr << "Failed to open file " << out_path << ", " << e.what(); 
    }

    auto logging_evs = split_string(Parameter("EventsToLog"));
    for (const auto& ev_name : logging_evs) {
        events_to_log.emplace_back(ev_name, 0);
    }

}


std::vector<std::string> split_string(std::string in) {
    std::vector<std::string> spl{};

    std::stringstream ss{in};
    std::string tmp{};
    while(ss.rdbuf()->in_avail()) {
        std::getline(ss, tmp, ' ');
        spl.push_back(tmp);
        tmp.clear();
    }
    return spl;
}
