#ifndef TIME_LOGGING_FILTER_H
#define TIME_LOGGING_FILTER_H
#include "GenericFilter.h"
#include <fstream>
#include <vector>

class EventChangeLoggingFilter : GenericFilter {
  public:
    EventChangeLoggingFilter();
    ~EventChangeLoggingFilter();

    void Publish() override;
    void Preflight( const SignalProperties&, SignalProperties& ) const override;
    void Initialize( const SignalProperties&, SignalProperties& );
    void Process( const GenericSignal&, GenericSignal& ) override; 

  private:
    std::ofstream out_file;
    std::vector<std::pair<std::string, unsigned int>> events_to_log;
    int sample_block_size;
};
#endif
