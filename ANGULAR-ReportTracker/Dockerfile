FROM node:latest
WORKDIR /usr/src/app
COPY package.json package-lock.json ./
RUN npm install
RUN npm install -g @angular/cli
COPY . .
CMD ng serve --port $PORT --host 0.0.0.0 --disable-host-check true
